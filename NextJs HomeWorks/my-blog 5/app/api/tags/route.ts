import { NextResponse } from "next/server";
import { blog } from "@/data/blog";

export async function GET() {
  // Extract all unique tags from the blog data
  const allTags = blog.reduce<string[]>((acc, post) => {
    post.tags.forEach((tag) => {
      if (!acc.includes(tag)) {
        acc.push(tag);
      }
    });
    return acc;
  }, []);

  const response = NextResponse.json({ tags: allTags });

  // Add CORS headers for the public API
  response.headers.set("Access-Control-Allow-Origin", "*");
  response.headers.set("Access-Control-Allow-Methods", "GET, OPTIONS");
  response.headers.set("Access-Control-Allow-Headers", "Content-Type");

  return response;
}

export async function OPTIONS() {
  const response = new NextResponse(null, { status: 204 });
  
  response.headers.set("Access-Control-Allow-Origin", "*");
  response.headers.set("Access-Control-Allow-Methods", "GET, OPTIONS");
  response.headers.set("Access-Control-Allow-Headers", "Content-Type");
  
  return response;
}
