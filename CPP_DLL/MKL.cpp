#include "pch.h"
#include <time.h>
#include <mkl.h>

extern "C"  _declspec(dllexport)
void VMS_Ln(MKL_INT n, float* x, float* y_HA, float* y_EP, double* time, int& ret)
{
	try
	{
		clock_t start_HA = clock();
		vmsLn(n, x, y_HA, VML_HA);
		clock_t finish_HA = clock();
		time[0] = (double)(finish_HA - start_HA) / (CLOCKS_PER_SEC * CLOCKS_PER_SEC);

		clock_t start_EP = clock();
		vmsLn(n, x, y_EP, VML_EP);
		clock_t finish_EP = clock();
		time[1] = (double)(finish_EP - start_EP) / (CLOCKS_PER_SEC * CLOCKS_PER_SEC);

		ret = 0;
	}
	catch (...)
	{
		ret = -1;
	}
}

extern "C"  _declspec(dllexport)
void VMD_Ln(MKL_INT n, double* x, double* y_HA, double* y_EP, double* time, int& ret)
{
	try
	{
		clock_t start_HA = clock();
		vmdLn(n, x, y_HA, VML_HA);
		clock_t finish_HA = clock();
		time[0] = (double)(finish_HA - start_HA) / (CLOCKS_PER_SEC * CLOCKS_PER_SEC);

		clock_t start_EP = clock();
		vmdLn(n, x, y_EP, VML_EP);
		clock_t finish_EP = clock();
		time[1] = (double)(finish_EP - start_EP) / (CLOCKS_PER_SEC * CLOCKS_PER_SEC);

		ret = 0;
	}
	catch (...)
	{
		ret = -1;
	}
}

extern "C"  _declspec(dllexport)
void VMS_LGamma(MKL_INT n, float* x, float* y_HA, float* y_EP, double* time, int& ret)
{
	try
	{
		clock_t start_HA = clock();
		vmsLGamma(n, x, y_HA, VML_HA);
		clock_t finish_HA = clock();
		time[0] = (double)(finish_HA - start_HA) / (CLOCKS_PER_SEC * CLOCKS_PER_SEC);

		clock_t start_EP = clock();
		vmsLGamma(n, x, y_EP, VML_EP);
		clock_t finish_EP = clock();
		time[1] = (double)(finish_EP - start_EP) / (CLOCKS_PER_SEC * CLOCKS_PER_SEC);

		ret = 0;
	}
	catch (...)
	{
		ret = -1;
	}
}

extern "C"  _declspec(dllexport)
void VMD_LGamma(MKL_INT n, double* x, double* y_HA, double* y_EP, double* time, int& ret)
{
	try
	{
		clock_t start_HA = clock();
		vmdLGamma(n, x, y_HA, VML_HA);
		clock_t finish_HA = clock();
		time[0] = (double)(finish_HA - start_HA) / (CLOCKS_PER_SEC * CLOCKS_PER_SEC);

		clock_t start_EP = clock();
		vmdLGamma(n, x, y_EP, VML_EP);
		clock_t finish_EP = clock();
		time[1] = (double)(finish_EP - start_EP) / (CLOCKS_PER_SEC * CLOCKS_PER_SEC);

		ret = 0;
	}
	catch (...)
	{
		ret = -1;
	}
}