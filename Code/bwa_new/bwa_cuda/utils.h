#if defined ( WIN32 ) || defined ( WIN64 )
#define __func__ __FUNCTION__
#include <stdio.h>
#endif

#ifndef __func__
#define __func__ __FUNCTION__
#endif

#define err_fatal_simple(msg) _err_fatal_simple(__func__, msg)
#define err_fatal_simple_core(msg) _err_fatal_simple_core(__func__, msg)

#define xopen(fn, mode) err_xopen_core(__func__, fn, mode)
#define xreopen(fn, mode, fp) err_xreopen_core(__func__, fn, mode, fp)
#define xzopen(fn, mode) err_xzopen_core(__func__, fn, mode)

#define xassert(cond, msg) if ((cond) == 0) _err_fatal_simple_core(__func__, msg)

int err_fseek(FILE *stream, long offset, int whence);
size_t err_fread_noeof(void *ptr, size_t size, size_t nmemb, FILE *stream);
FILE *err_xopen_core(const char *func, const char *fn, const char *mode);
 long err_ftell(FILE *stream);
 int err_fclose(FILE *stream) ;

