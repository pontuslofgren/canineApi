
import {  useQuery} from "react-query";




function App() {


  // const queryClient = useQueryClient();
  const query = useQuery({queryKey:['getdogs'], queryFn: getDogs})
  
  async function getDogs(): Promise<Dog[]>{
    const response = await fetch("https://canineapplab.azurewebsites.net/dogs");
    const responseJson = await response.json() as Dog[];
    return responseJson;
  }


  if (query.isLoading) return (<p>Loading...</p>)
  if (query.error) return (<p>Something went wrong.</p>)
  return (
    <>
      {query.data?.map((dog) => {
        <p>{dog.name}</p>
      })}

      <h1>hello guys</h1>

    </>
  )

}

export default App

type Dog = {
  id: number;
  name: string;
}