import { NextResponse } from "next/server";

export const dynamic = "force-dynamic";

export async function GET() {
  // Имитация количества активных читателей (в реальном приложении — Redis / WebSocket)
  const count = Math.floor(Math.random() * 50) + 1;
  return NextResponse.json({ count });
}
