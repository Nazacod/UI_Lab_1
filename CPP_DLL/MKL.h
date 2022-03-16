#pragma once
#include <mkl.h>

extern "C"  _declspec(dllexport)
void VMS_Ln(MKL_INT n, float* x, float* y_HA, float* y_EP, double* time, int& ret);

extern "C"  _declspec(dllexport)
void VMD_Ln(MKL_INT n, double* x, double* y_HA, double* y_EP, double* time, int& ret);

extern "C"  _declspec(dllexport)
void VMS_LGamma(MKL_INT n, float* x, float* y_HA, float* y_EP, double* time, int& ret);

extern "C"  _declspec(dllexport)
void VMD_LGamma(MKL_INT n, double* x, double* y_HA, double* y_EP, double* time, int& ret);
