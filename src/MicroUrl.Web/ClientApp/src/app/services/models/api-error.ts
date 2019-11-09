export class ApiError extends Error {
  constructor(errors: string[]) {
    super(errors.join(', '));
    this.errors = errors;
  }

  public errors: string[];
}
