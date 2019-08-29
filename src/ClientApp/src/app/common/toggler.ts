export function Toggler<T>(toggleProperty: keyof T) {
  return function (_target: any, _propertyKey: string, descriptor: PropertyDescriptor) {
    const originalMethod = descriptor.value;
    descriptor.value = function (...args: any[]) {
      const setValue = (val: boolean) => this[toggleProperty] = val;
      setValue(true);
      const result = originalMethod.apply(this, args);
      if (result.then) {
        result.then(() => setValue(false));
      } else {
        setValue(false);
      }

      return result;
    };
  };
}
