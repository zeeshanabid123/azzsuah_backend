using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace Azzuha.Apis.DynamoDb
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject(myJsonResponse); 
    public class EgressEndpoints
    {
        public string CMAF { get; set; }
        public string DASH { get; set; }
        public string HLS { get; set; }
        public string MSS { get; set; }

    }

    public class AccelerationSettings
    {
        public string Mode { get; set; }

    }

    public class AudioSelector1
    {
        public string DefaultSelection { get; set; }
        public int Offset { get; set; }
        public int ProgramSelection { get; set; }
        public string SelectorType { get; set; }
        public List<int> Tracks { get; set; }

    }

    public class AudioSelectors
    {
        public AudioSelector1 AudioSelector1 { get; set; }

    }

    public class VideoSelector
    {
        public string ColorSpace { get; set; }
        public string Rotate { get; set; }

    }

    public class Input
    {
        public AudioSelectors AudioSelectors { get; set; }
        public string DeblockFilter { get; set; }
        public string DenoiseFilter { get; set; }
        public string FileInput { get; set; }
        public string FilterEnable { get; set; }
        public int FilterStrength { get; set; }
        public string PsiControl { get; set; }
        public string TimecodeSource { get; set; }
        public VideoSelector VideoSelector { get; set; }

    }

    public class FileGroupSettings
    {
        public string Destination { get; set; }

    }

    public class HlsGroupSettings
    {
        public string Destination { get; set; }
        public int MinSegmentLength { get; set; }
        public int SegmentLength { get; set; }

    }

    public class OutputGroupSettings
    {
        public FileGroupSettings FileGroupSettings { get; set; }
        public string Type { get; set; }
        public HlsGroupSettings HlsGroupSettings { get; set; }

    }

    public class ContainerSettings
    {
        public string Container { get; set; }

    }

    public class FrameCaptureSettings
    {
        public int FramerateDenominator { get; set; }
        public int FramerateNumerator { get; set; }
        public int MaxCaptures { get; set; }
        public int Quality { get; set; }

    }

    public class CodecSettings
    {
        public string Codec { get; set; }
        public FrameCaptureSettings FrameCaptureSettings { get; set; }

    }

    public class VideoDescription
    {
        public string AfdSignaling { get; set; }
        public string AntiAlias { get; set; }
        public CodecSettings CodecSettings { get; set; }
        public string ColorMetadata { get; set; }
        public string DropFrameTimecode { get; set; }
        public string RespondToAfd { get; set; }
        public string ScalingBehavior { get; set; }
        public int Sharpness { get; set; }
        public string TimecodeInsertion { get; set; }

    }

    public class Output
    {
        public ContainerSettings ContainerSettings { get; set; }
        public string NameModifier { get; set; }
        public VideoDescription VideoDescription { get; set; }

    }

    public class OutputGroup
    {
        public string Name { get; set; }
        public OutputGroupSettings OutputGroupSettings { get; set; }
        public List<Output> Outputs { get; set; }
        public string CustomName { get; set; }

    }

    public class TimecodeConfig
    {
        public string Source { get; set; }

    }

    public class Settings
    {
        public List<Input> Inputs { get; set; }
        public List<OutputGroup> OutputGroups { get; set; }
        public TimecodeConfig TimecodeConfig { get; set; }

    }

    public class UserMetadata
    {
        public string guid { get; set; }
        public string workflow { get; set; }

    }

    public class EncodingJob
    {
        public AccelerationSettings AccelerationSettings { get; set; }
        public string JobTemplate { get; set; }
        public string Role { get; set; }
        public Settings Settings { get; set; }
        public UserMetadata UserMetadata { get; set; }

    }

    public class VideoDetails
    {
        public int heightInPx { get; set; }
        public int widthInPx { get; set; }

    }

    public class OutputDetail
    {
        public int durationInMs { get; set; }
        public List<string> outputFilePaths { get; set; }
        public VideoDetails videoDetails { get; set; }

    }

    public class OutputGroupDetail
    {
        public List<OutputDetail> outputDetails { get; set; }
        public string type { get; set; }
        public List<string> playlistFilePaths { get; set; }

    }

    public class UserMetadata2
    {
        public string guid { get; set; }
        public string workflow { get; set; }

    }

    public class Detail
    {
        public string accountId { get; set; }
        public string jobId { get; set; }
        public List<OutputGroupDetail> outputGroupDetails { get; set; }
        public string queue { get; set; }
        public string status { get; set; }
        public long timestamp { get; set; }
        public UserMetadata2 userMetadata { get; set; }

    }

    public class EncodingOutput
    {
        public string account { get; set; }
        public Detail detail { get; set; }
        //public string detail-type { get; set; }
        public string id { get; set; }
        public string region { get; set; }
        public List<string> resources { get; set; }
        public string source { get; set; }
        public string time { get; set; }
        public string version { get; set; }

    }

    [DynamoDBTable("MessageMuse-VOD-Stack")]
    public class VideoStackItemModel
    {
        public string acceleratedTranscoding { get; set; }
        public string archiveSource { get; set; }
        public string cloudFront { get; set; }
        public string destBucket { get; set; }
        public EgressEndpoints egressEndpoints { get; set; }
        public bool enableMediaPackage { get; set; }
        public bool enableSns { get; set; }
        public bool enableSqs { get; set; }
        public string encodeJobId { get; set; }
        public EncodingJob encodingJob { get; set; }
        public EncodingOutput encodingOutput { get; set; }
        public int encodingProfile { get; set; }
        public string endTime { get; set; }
        public bool frameCapture { get; set; }
        public int frameCaptureHeight { get; set; }
        public int frameCaptureWidth { get; set; }
        [DynamoDBHashKey]
        public string guid { get; set; }
        public string hlsPlaylist { get; set; }
        public string hlsUrl { get; set; }
        public string inputRotate { get; set; }
        public bool isCustomTemplate { get; set; }
        public string jobTemplate { get; set; }
        public string jobTemplate_1080p { get; set; }
        public string jobTemplate_2160p { get; set; }
        public string jobTemplate_720p { get; set; }
        public List<string> mp4Outputs { get; set; }
        public List<string> mp4Urls { get; set; }
        public string myCustomProperty { get; set; }
        public string srcBucket { get; set; }
        public int srcHeight { get; set; }
        public string srcMediainfo { get; set; }
        public string srcMetadataFile { get; set; }
        public string srcVideo { get; set; }
        public int srcWidth { get; set; }
        public string startTime { get; set; }
        public List<string> thumbNails { get; set; }
        public List<string> thumbNailsUrls { get; set; }
        public string workflowName { get; set; }
        public string workflowStatus { get; set; }
        public string workflowTrigger { get; set; }

    }


}
