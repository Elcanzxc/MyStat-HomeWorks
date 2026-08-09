import { NextRequest, NextResponse } from 'next/server'



let notes:{id:String,text:string}[]=[
    {id:'1',text:"text1"},
]

export async function GET() {
  return (
    NextResponse.json(notes)
  )
}


export async function POST(request:NextRequest){
    const body = await request.json()
    const newNote = {id:String(Date.now()),text:body.text}
    
    notes.push(newNote)
    return NextResponse.json(newNote,{status:201})
} 