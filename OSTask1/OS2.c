#include <stdio.h>
#include <sys/fcntl.h>

int main(int argc, char* argv[])
{
	if (argc != 2) { return -1; }

	char* filePath = argv[1];
	int fileFD = open(filePath, O_RDONLY);

	if (fileFD == -1) {
		close(fileFD);
		return -1;
	}

	const char* rowsFromFile[3];
	char buff = '0';

	for (int i = 0; i < 3; i++)
	{
		do {
			int readFile = read(fileFD, &buff, 1);
			if (readFile != 1) { 
				close(fileFD);
				return -1;
			}
			strncat(rowsFromFile[i], &buff, 1);			
		} while (buff != '\n' && buff != '\0');		
	}
	
	int commandsFile = open("file.txt", O_WRONLY | O_CREAT | O_TRUNC, 0666);
	int pid = fork();
	char* execvpArr[3];
	execvpArr[0] = "ls";
	execvpArr[1] = rowsFromFile[0];
	execvpArr[2] = NULL;

	if (pid==0)
	{
		close(1);				/* close stdout*/
		dup(commandsFile);		/* dup will copy fd into stdout */
		close(commandsFile);	/* no need for fd anymore*/
		execvp(execvpArr[0], execvpArr);
	}







}
