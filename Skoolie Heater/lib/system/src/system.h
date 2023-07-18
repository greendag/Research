#ifndef SYSYEM_H_
#define SYSYEM_H_

#define PANIC(...) abort()

class System
{
    public:
        System() {} 
        void Log(const char *tag, const char *format, ...);
        long GetChipId();
        int GetResetReasonCode(int core);
        const char *GetResetReason(int core);
    
    private:
        const char *_module = "system";
};

#endif