## AWS S3 Wrapper Class.

### Usage ####

This class was designed to interface with AWS S3 and allows users minimal setup.  Currently, there are five methods available.  They are NewS3Connection,ListBucketContents, GetS3Item, PutS3Item and DeleteS3Item.

NewS3Connection is the start point for the class.  The method requires the Amazon.RegionEndpoint so the class knows what region to look into in AWS.

```
AWSS3Wrapper s3 = AWSS3Wrapper.NewS3Connection(Amazon.RegionEndpoint.USWest2);
```

ListBucketContents is simply to use and only requires the bucket name.  The method returns a Task<List<string>>.  

Note - This will also return any file paths as contents.

```
List<string> contents = await s3.ListBucketContents(<My s3 bucket name>);
```

GetS3Items requires both the bucket name and key(or path if needed, see below in Special Notes).  The method returns a generic.  

Note - This was developed using a generic, since I mostly wanted to model JSON data to custom objects.  This works well with .csv files.  If you are trying to use it for something else, you may need modify the existing method to handle you specific needs.

```
MyObject s3Item = await s3.GetS3Item<MyObject>(<My s3 bucket name>, <My s3 key>);
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