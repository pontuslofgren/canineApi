
import {  useMutation, useQuery, useQueryClient} from "react-query";
import { useForm, SubmitHandler } from "react-hook-form"




function App() {


  const queryClient = useQueryClient();
  const query = useQuery({queryKey:['getdogs'], queryFn: getDogs})
  
  const mutation = useMutation({
    mutationFn: postDog, 
    onSuccess: () => {
      queryClient.invalidateQueries({queryKey: ['getdogs']})
    }
  })
  
  async function getDogs(): Promise<Dog[]>{
    const response = await fetch("https://canineapplab.azurewebsites.net/dogs");
    const responseJson = await response.json() as Dog[];
    return responseJson;
  }

  async function postDog(name: string){
    await fetch("https://canineapplab.azurewebsites.net/dogs", {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify({name: name})
    });
    
  }

  const {
    register,
    handleSubmit,
  } = useForm<Inputs>()
  const onSubmit: SubmitHandler<Inputs> = (data) => {
    mutation.mutate(data.name);
  }


  if (query.isLoading) return (<p>Loading...</p>)
  if (query.error) return (<p>Something went wrong.</p>)
  return (
    <>
<form onSubmit={handleSubmit(onSubmit)}>
      <input defaultValue="Dog name" {...register("name")} />
      <input type="submit" />
    </form>
      {query.data?.map((dog) => {
        return(
        <p>{dog.name}</p>
      )
      })}
    </>
  )

}

export default App

type Dog = {
  id: number;
  name: string;
}

type Inputs = {
  name: string
}