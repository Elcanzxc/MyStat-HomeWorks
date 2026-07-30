import { revalidate } from "@/app/page";


export default async function RickAndMortyApi() {
//   const resources = await fetch('https://rickandmortyapi.com/api/character',{cache:'no-store'},{next:{revalidate:60}});
 const resources = await fetch('https://rickandmortyapi.com/api/character',{next:{revalidate:60}});
  const data = await resources.json();

  return (
    <div>
     {data.results.map(({ name,id }: { name: string , id:number}) => <div key={id}>{name}</div>)}
    </div>
 
  )
}
