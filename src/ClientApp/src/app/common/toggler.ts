export function Toggler<T>(toggleProperty: keyof T) {
  return function (_target: any, _propertyKey: string, descriptor: PropertyDescriptor) {
    const originalMethod = descriptor.value;
    descriptor.value = function (...args: any[]) {
      const setValue = (val: boolean) => (this as any)[toggleProperty as string] = val;
      setValue(true);
      let result = null;
      try {
        result = originalMethod.apply(this, args);
      } catch (er) {
        result = er;
      }
      if (result.then) {
        result.then(() => setValue(false)).catch(() => setValue(false));
      } else {
        setValue(false);
      }

      return result;
    };
  };
}
