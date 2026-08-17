"use server";

import { revalidatePath } from "next/cache";
import { Note } from "./api/route";

export async function GetNotes() {
  const jsonNotes = await fetch(`${process.env.BASE_URL}/apiTest/api`);

  return await jsonNotes.json();
}

export async function PostNotes(text: string) {
  await fetch(`${process.env.BASE_URL}/apiTest/api`, {
    method: "POST",
    body: JSON.stringify({ text }),
  });

  revalidatePath("/apiTest");
}

export async function PutNotes(note: Note) {
  console.log(`PutNotes: ${note.text}:${note.id}`)
  await fetch(`${process.env.BASE_URL}/apiTest/api/${note.id}`, {
    method: "PUT",
    body: JSON.stringify(note.text),
  });

  revalidatePath("/apiTest");
}


export async function DeleteNotes(id:string) {
 
  await fetch(`${process.env.BASE_URL}/apiTest/api/${id}`, {
    method: "DELETE",
  });

  revalidatePath("/apiTest");
}

