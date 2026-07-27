import NoteCreate from "@/components/NoteCreate"

export default async function DemoNotesPage(){
   const base_url = process.env.BASE_URL
   const response = await fetch(`${base_url}/api/demo-routes`)
   const data = await response.json()
    return(
       <NoteCreate/>
    )
}