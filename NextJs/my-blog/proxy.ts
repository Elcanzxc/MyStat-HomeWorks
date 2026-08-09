
import { NextRequest, NextResponse } from "next/server";

function proxy(request:NextRequest) {
 
  const authHeader = request.headers.get('authorization')

  if(authHeader !== 'BearerToken'){

    return NextResponse.json({error:"Нет Токена"},{status:401})
  }

  return NextResponse.next();
}

export default proxy;

export const config = {
  matcher: ["/test"],
};
