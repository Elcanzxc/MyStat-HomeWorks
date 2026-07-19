import { NextResponse ,NextRequest} from "next/server";

let notes: { id: string; text: string }[] = [
  { id: "1", text: "Первая заметка" },
  { id: "2", text: "Вторая заметка" },
];

export async function GET() {
  return NextResponse.json({ text: "Hello" });
}

export async function POST(request:NextRequest){
    const body = await request.json()
    const newNote = {id:String(Date.now()),text:body.text}
    notes.push(newNote)
    return NextResponse.json(newNote,{status:201})
}