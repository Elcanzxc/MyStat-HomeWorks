
'use client'

import { useSearchParams } from "next/navigation"
import { useState } from "react"
import { json } from "stream/consumers"

export default function NoteCreate(){
   const [text,setText] = useState('')

   const add = async () =>{
    try{
        const request = fetch(`${process.env.BASE_URL}/api/demo-routes`,{method:'POST' , body:JSON.stringify({text})})
    }
    catch(e){
        console.error(e)
    }
   }
    return(
        <div>
            <input value={text} onChange={(e) =>setText(e.target.value)} placeholder="text"/>
            <button>Add</button>
        </div>
    )
}