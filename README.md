## AWS S3 Wrapper Class.

### Usage ####

This class was designed to interface with AWS S3 and allows users minimal setup.  

Currently, there are four methods available.  They are ListBucketContents, GetS3Item, PutS3Item and DeleteS3Item.

ListBucketContents is simply to use and only requires the bucket name.  The method returns a Task<List<string>>.  

Note - This will also return any file paths as contents.

```
List<string> contents = await ListBucketContents(<My s3 bucket name>);
```

### Setup ###

To use this in projects, you will have to have setup your AWS Credentials through the AWS CLI.

You will also need to install the AWSSDK.S3 package.  The two packages at the top of AWSS3Wrapper are commented out, you will need to un-comment them.

That is it.  You can simply copy and paste this in and it will work.

### Special Notes ###

If you are looking for specific paths or files, it is possible to find them by updating the key.  Below, I want to access file3.mp3. To do that, I would use the update the key to have both the path and file name.

```
myBucket
    - file1.txt
    - file2.txt
    - subFolder
        - file3.txt

string myFile = await GetS3Item(bucketName: "myBucket", key: "subfolder/file3.txt");
```


### Comments, Questions, or Suggestions? ###

Please feel free to reach out to me at tausti0065@gmail.com.