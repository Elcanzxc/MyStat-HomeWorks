'use client'
import { DeleteNotes } from "./action"

export default function DeleteNoteForm({id}:{id:string}){

    const deleteNote = async() => {
       await DeleteNotes(id)
    }


    return ( <button onClick={deleteNote}>Udalit</button>
  )
}