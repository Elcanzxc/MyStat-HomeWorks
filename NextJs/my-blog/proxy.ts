import { NextResponse } from "next/server";

function proxy() {
  return NextResponse.next();
}

export default proxy;

export const config = {
  matcher: ["/"],
};
