import { NextRequest, NextResponse } from "next/server";

export function withAuth(handler: (req: NextRequest, ...args: any[]) => Promise<NextResponse> | NextResponse) {
  return async (req: NextRequest, ...args: any[]) => {
    const authHeader = req.headers.get("Authorization");

    if (!authHeader || !authHeader.startsWith("Bearer ")) {
      return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
    }

    // You can add token validation logic here if needed.
    // For now, it just checks for the presence of the Bearer token.

    return handler(req, ...args);
  };
}
