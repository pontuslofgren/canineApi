import { Dog } from "./App";

export default function DogCard({ dog }: Props) {
  return (
    <>
      <div className="card w-96 bg-base-100 shadow-xl m-5">
        <figure><img src={dog.imageUrl} /></figure>
        <div className="card-body">
          <h2 className="card-title">{dog.name}</h2>
          <div className="card-actions justify-end">
            <button className="btn btn-primary">Delete</button>
          </div>
        </div>
      </div>
    </>
  )
}


type Props = {
  dog: Dog;
}