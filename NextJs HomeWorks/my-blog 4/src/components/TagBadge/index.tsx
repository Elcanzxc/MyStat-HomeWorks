import Link from "next/link";

interface TagBadgeProps {
  tag: string;
}

export default function TagBadge({ tag }: TagBadgeProps) {
  return (
    <Link 
      href={`/blog/tags/${tag}`}
      className="inline-block px-3 py-1 mr-2 mb-2 text-sm font-semibold rounded-full bg-brand text-white dark:bg-brand dark:text-gray-100 hover:opacity-80 transition-opacity"
    >
      #{tag}
    </Link>
  );
}
