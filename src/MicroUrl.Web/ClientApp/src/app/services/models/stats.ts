export interface VisitorRanking {
  from: Date;
  to: Date;
  visitors: number;
  uniqueVisitors: number;
}

export interface Stats {
  key: string;
  targetUrl: string;
  allTime: VisitorRanking;
  recents: VisitorRanking[];
}
