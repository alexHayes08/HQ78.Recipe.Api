export class RecursiveQuery<T> {
    [TKey in keyof T]?: boolean;
}
