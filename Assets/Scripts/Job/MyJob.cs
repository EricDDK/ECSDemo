﻿using Unity.Collections;
using Unity.Jobs;

public struct MyJob : IJob
{
	public float a;
	public float b;
	public NativeArray<float> result;

	public void Execute()
	{
		result[0] = a + b;
	}
}

public class Test
{
	public void Do()
	{
		// Create a native array of a single float to store the result. This example waits for the job to complete for illustration purposes
		NativeArray<float> result = new NativeArray<float>(1, Allocator.TempJob);

		// Set up the job data
		MyJob jobData = new MyJob();
		jobData.a = 10;
		jobData.b = 10;
		jobData.result = result;

		// Schedule the job
		JobHandle handle = jobData.Schedule();

		// Wait for the job to complete
		handle.Complete();

		// All copies of the NativeArray point to the same memory, you can access the result in "your" copy of the NativeArray
		float aPlusB = result[0];

		// Free the memory allocated by the result array
		result.Dispose();
	}
}