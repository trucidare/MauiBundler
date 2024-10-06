import type { DotnetRef } from "./index";
export declare class PluginBase {
    namespace: string;
    dotnetRef: DotnetRef;
    constructor();
    initialize(namespace: any, dotnetRef: any): void;
    invokeMethodAsync(name: any, ...args: any[]): Promise<unknown>;
}
