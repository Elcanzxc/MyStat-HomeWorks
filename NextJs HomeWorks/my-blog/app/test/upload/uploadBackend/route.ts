// import { NextRequest, NextResponse } from "next/server";

import { NextRequest, NextResponse } from "next/server"

// export async function POST(params: NextRequest) {
//   const formData = await params.formData();

//   const file = formData.get("file") as File;

//   const bytes = await file.arrayBuffer();

//   return NextResponse.json({
//     name: file.name,
//     sizeBytes: bytes.byteLength,
//     type: file.type,
//   });
// }


export async function POST(formData:NextRequest){
  
  const request = await formData.formData()

  const data =  request.get('file') as File

  const bytes = await data.arrayBuffer();

  return NextResponse.json({
    name: data.name,
    sizeBytes: bytes.byteLength,
    type: data.type,
  });
}