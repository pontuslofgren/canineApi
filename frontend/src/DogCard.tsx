import { Dog } from "./App";

export default function DogCard({ dog }: Props) {
  return (
    <>
      <div>
        <p>{dog.name}</p>
        <img src={dog.imageUrl} />
      </div>
    </>
  )
}


type Props = {
  dog: Dog;
}