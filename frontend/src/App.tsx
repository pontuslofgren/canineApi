
import { useMutation, useQuery, useQueryClient } from "react-query";
import { useForm, SubmitHandler } from "react-hook-form"

function App() {

  const queryClient = useQueryClient();
  const query = useQuery({ queryKey: ['getdogs'], queryFn: getDogs })

  const mutation = useMutation({
    mutationFn: postDog,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['getdogs'] })
    }
  })

  async function getDogs(): Promise<Dog[]> {
    const response = await fetch("https://canineapplab.azurewebsites.net/dogs");
    const responseJson = await response.json() as Dog[];
    console.log(responseJson)
    return responseJson;
  }

  async function postDog(data: Inputs) {
    const formData = new FormData();
    formData.append("Name", data.name);
    formData.append("Image", data.image[0]);
    console.log(formData);

    const response = await fetch("https://canineapplab.azurewebsites.net/dogs", {
      method: 'POST',
      body: formData
    });

    console.log(response);
  }

  const {
    register,
    handleSubmit,
  } = useForm<Inputs>()
  const onSubmit: SubmitHandler<Inputs> = (data) => {
    mutation.mutate(data);
  }

  if (query.isLoading) return (<p>Loading...</p>)
  if (query.error) return (<p>Something went wrong.</p>)
  return (
    <>
      <form onSubmit={handleSubmit(onSubmit)}>
        <input defaultValue="Dog name" {...register("name")} />
        <input type="file" {...register("image")} />
        <input type="submit" />
      </form>
      {query.data?.map((dog) => {
        return (
          <div>
            <p>{dog.name}</p>
            <img src={dog.imageUrl} />
          </div>
        )
      })}
    </>
  )

}

export default App

type Dog = {
  id: number;
  name: string;
  imageUrl: string;
}

type Inputs = {
  name: string;
  image: FileList
}