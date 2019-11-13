#include <stdio.h>
#include <sys/fcntl.h>

int main(int argc, char* argv[])
{
	if (argc != 3) { return 0; }

	//getting the arguments from the commang line
	char* firstFilePath = argv[1];
	char* secondFilePath = argv[2];

	//open the files for reading
	int firstFileFD = open(firstFilePath, O_RDONLY);
	int secondFileFD = open(secondFilePath, O_RDONLY);

	//check that we really find the files
	if (firstFileFD == -1 || secondFileFD == -1) { return 0; }

	//declare the buffer's for both of the files
	char firstFileBuff, secondFileBuff;
	//declare the readed bytes
	int readedFromFirstFile = 0;
	int readedFromSecondFile = 0;

	do
	{
		firstFileBuff = 0;
		secondFileBuff = 0;
		readedFromFirstFile = read(firstFileFD, &firstFileBuff, 1);
		readedFromSecondFile = read(secondFileFD, &secondFileBuff, 1);

		if (firstFileBuff != secondFileBuff)
		{
			close(firstFileFD);
			close(secondFileFD);
			return 1;
		}
	} while (readedFromFirstFile && readedFromSecondFile);

	close(firstFileFD);
	close(secondFileFD);
	return 2;
}
