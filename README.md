# MusicApi
A rest api project using Asp.Net Core Web Api

The project mainly deals with various crud operations. It stores and retrives data regarding songs, artists, and albums.

For files such as images(png, jpg, jpeg) and audio files(mp3) a storage service was needed. The project directory has been 
used as a mean of storing these files. 

In case of uploading, the program writes the file in a specific directory and assigns a unique guid as the file name for that 
file. In case of sending that file as api response, the file is read as byte stream and sent as api response in base64 format
so that the client side can work with that response.

## Endpoints

  ### Songs
    - {{baseUrl}}/api/Songs/AddSong 
    - {{baseUrl}}/api/Songs?pageNumber=1&pageSize=2
    - {{baseUrl}}/api/Songs/FeaturedSongs
    - {{baseUrl}}/api/Songs/NewSongs
    - {{baseUrl}}/api/Songs/SearchSongs?searchKey=test

 ### Artists
    - {{baseUrl}}/api/Artists/AddArtist
    - {{baseUrl}}/api/Artists
    - {{baseUrl}}/api/Artists/GetArtistDetails?artistId=3d327948-eb79-4278-70fa-08dbdbc6695c

 ### Albums
    - {{baseUrl}}/api/Albums/AddAlbum
    - {{baseUrl}}/api/Albums
    - {{baseUrl}}/api/Albums/GetAlbumDetails?albumId=8190bcd8-4b27-476f-d6c3-08dbdc59f6ef

 ### Utility
    - {{baseUrl}}/api/Utility/GetImage?imageId=5d53c01d-a3bf-4142-a22f-838b75bf306c
    - {{baseUrl}}/api/Utility/GetAudio?fileId=d4e24bac-9e4f-4ae7-b7cd-3d04d6d1b47e

