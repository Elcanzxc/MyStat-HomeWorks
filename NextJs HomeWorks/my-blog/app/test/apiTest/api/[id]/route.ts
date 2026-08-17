import { NextRequest, NextResponse } from "next/server";
import { Delete, Notes } from "../route";

export async function PUT(
  request: NextRequest,
  { params }: { params: { id: string } },
) {

  const { id } = await params;

  const text = await request.json();

    console.log(`PUT: ${text}:${id}`)

  const note = Notes.find((item) => item.id === id);

  if (note === undefined) {
    return NextResponse.json({ error: "not-found" }, { status: 404 });
  }
  note.text = text;
  return NextResponse.json({edit:id})
}

export async function DELETE(
  request: NextRequest,
  { params }: { params: { id: string } },
) {

  const { id } = await params;

  Delete(id)

  return NextResponse.json({status:200})
}
