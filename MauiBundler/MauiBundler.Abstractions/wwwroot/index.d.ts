export interface DotnetRef {
    invokeMethodAsync(name: string, ...args: unknown[]): Promise<unknown>;
}
