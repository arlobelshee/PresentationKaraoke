rm -f .\Presentation.karaoke
compress-archive -path .\Presentation\*.* -DestinationPath Presentation.karaoke.zip -CompressionLevel Optimal
mv .\Presentation.karaoke.zip .\Presentation.karaoke
