import { NextResponse } from "next/server";



let count = 0;

export async function GET(){
    
    count += Math.random() > 0.5? 0 : 1
 
    return NextResponse.json({count})
}