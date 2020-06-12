#include "iostream"
#include <time.h>

using namespace std;

int main()
{
	const int rows = 1000, cols = 1000, MAX_LOOP = 1000, STEP = 1;
	int i, j, n;

	int** a = new int*[rows];
	for (i = 0; i < rows; ++i)
	{
		a[i] = new int[cols];
		for (j = 0; j < cols; ++j)
		{
			a[i][j] = i + j;
		}
	}

	clock_t start, end;

	n = MAX_LOOP;
	start = clock();
	// cache miss
	while (n-- > 0)
	{
		for (j = 0; j < cols; j += STEP)
		{
			for (i = 0; i < rows; i += STEP)
			{
				a[i][j] = 0xd5;
			}
		}
	}
	end = clock();
	cout << "cache miss: " << (double)(end - start) << endl;

	n = MAX_LOOP;
	start = clock();
	// cache hit
	while (n-- > 0)
	{
		for (i = 0; i < rows; i += STEP)
		{
			for (j = 0; j < cols; j += STEP)
			{
				a[i][j] = 0xff;
			}
		}
	}
	end = clock();
	cout << "cache hit: " << (double)(end - start) << endl;

	system("pause");
}