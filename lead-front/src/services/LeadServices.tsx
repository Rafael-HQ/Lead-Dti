export function formatLeadDate(isoDate: string) {
    const date = new Date(isoDate);

    const monthDay = new Intl.DateTimeFormat('en-US', {
      month: 'long',
      day: 'numeric'
    }).format(date);

    const time = new Intl.DateTimeFormat('en-US', {
      hour: 'numeric',
      minute: '2-digit',
      hour12: true
    }).format(date);

    return `${monthDay} @ ${time}`;
  }