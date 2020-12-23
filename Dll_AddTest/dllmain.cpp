// dllmain.cpp : 定义 DLL 应用程序的入口点。
#include "pch.h"
#include "dll_demo.h"

BOOL APIENTRY DllMain( HMODULE hModule,//DLL模块的句柄
                       DWORD  ul_reason_for_call,//调用本函数的原因
                       LPVOID lpReserved//保留
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        //进程正在加载本DLL
        break;
    case DLL_THREAD_ATTACH:
        //一个线程被创建
        break;
    case DLL_THREAD_DETACH:
        //一个线程正常退出
        break;
    case DLL_PROCESS_DETACH:
        //进程正在卸载本DLL
        break;
    }
    return TRUE;//返回TRUE,表示成功执行本函数
}

int Add(int a, int b)
{
    return (a + b);
}

