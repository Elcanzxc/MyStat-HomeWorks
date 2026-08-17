import { NextResponse } from 'next/server';
import { articles } from '@/src/utils';

// Это пример как бы был CORS политики в моем API, так как я теги достаю примиком из массива то api там не используется
export async function GET() {

  const tagsSet = new Set<string>();
  articles.forEach(article => {
    article.tags.forEach(tag => tagsSet.add(tag));
  });
  
  const tags = Array.from(tagsSet);
  
  return NextResponse.json(tags, {
    headers: {
      'Access-Control-Allow-Origin': '*', 
      'Access-Control-Allow-Methods': 'GET, OPTIONS', 
      'Access-Control-Allow-Headers': 'Content-Type, Authorization',
    },
  });
}

export async function OPTIONS() {
  return new NextResponse(null, {
    status: 200,
    headers: {
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Methods': 'GET, OPTIONS',
      'Access-Control-Allow-Headers': 'Content-Type, Authorization',
    },
  });
}
