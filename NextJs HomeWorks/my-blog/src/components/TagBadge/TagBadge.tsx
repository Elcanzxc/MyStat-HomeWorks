export default function TagBadge({ text }: { text: string }) {
  return (
    <span className="bg-tag-bg text-tag-text dark:bg-tag-bg-dark dark:text-tag-text-dark px-2.5 py-1 rounded-md text-xs font-semibold inline-block">
      {text}
    </span>
  );
}
