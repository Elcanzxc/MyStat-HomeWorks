import { NextRequest, NextResponse } from "next/server";
import { withAuth } from "@/utils/proxy";

async function handler(req: NextRequest) {
  // Logic for creating or handling a comment
  return NextResponse.json({ message: "Comment handled successfully" });
}

export const POST = withAuth(handler);
export const GET = withAuth(handler);
