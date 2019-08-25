export const CONFIG_TOKEN = 'CONFIG';

export interface MicroUrlSettings {
  AnalyticsId: string;
}

export interface Config {
  MicroUrlSettings: MicroUrlSettings;
}
