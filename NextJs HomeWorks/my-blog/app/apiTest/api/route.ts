import { error } from "console";
import { NextRequest, NextResponse } from "next/server";

export type Note = {
  id: string;
  text: string;
};

export let Notes: Note[] = [
  { id: "1", text: "Text1" },
  { id: "2", text: "Text2" },
];

export async function GET() {
  return NextResponse.json(Notes);
}

export async function POST(request: NextRequest) {
  
  const body = await request.json();
  const newNote = {
    id: Date.now().toString(),
    text: body.text,
  };
  Notes.push(newNote);

  return NextResponse.json(newNote, { status: 201 });
}

export async function Delete(id:string){
      Notes = Notes.filter((item) => item.id !== id);
}
