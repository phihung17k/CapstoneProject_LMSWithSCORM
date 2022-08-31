### Configuration Visual Studio

- Open **Properties** in LMS.API project
  
  - In **Build** tab, unchecked **Optimize code** in **General** section.
  
  - In **Build** tab, click **Advanced..** in **Output** section, select **Full** in drop down **Debugging information**

- In Tab bar, find **Debug** -> **Options** -> **Debugging** -> **General** and check **Suppress JIT optimization on module load (Managed only)**



### Error Code - EC

- 0: No error

- 403: Data Model Element Value Not Initialized

- 404: Data Model Element Is Read Only

- 405: Data Model Element Is Write Only

- 406: Data Model Element Type Mismatch

- 407: Data Model Element Value Out Of Range

- SPM: Smallest Permitted Maximum Exceeded (độ dài của giá trị phải nhỏ hơn SPM)

### scorm_core

1. **Location** - **cmi.location**
   
   - Read / Write . 
   
   - Mandatory
   
   - Data type: characterstring (SPM: 1000)
   
   - Format của giá trị do nhà phát triển SCO quyết định dựa theo ISO-10646-1 - Unicode
   
   - Đánh dấu điểm/trang/slide học cuối cùng trong file SCORM, có thể dùng làm điểm khởi đầu khi tham gia lại vào SCO sau một phiên học bị đình chỉ  - **a suspended learner session**
   
   - **GetValue("cmi.location")**: return characterstring, EC = 0
     
     - If "cmi.location" is requested before be initialized, return empty **""**, EC = 403  
   
   - **SetValue("cmi.location","chkPt1.p3.f5")**: return true, EC = 0

2. **Mode** - **cmi.mode**
   
   - Read - only
   
   - Mandatory
   
   - Data Type: state (browse, normal, review)
   
   - Identify one of three possible modes in which the SCO may be presented to the learner:
     
     - **browse**: **No** record any information about current learner session
     
     - **normal**: record information about current learner session - **Default**
     
     - **review**: **No** update information about current learner session to the recorded information
   
   - **GetValue("cmi.mode")**: Default return normal, EC = 0
   
   - **SetValue()**:  return false, EC = 404

3. **Credit** - **cmi.credit**
   
   - Read - only
   
   - Mandatory
   
   - Data Type: state (credit, no_credit)
   
   - Chỉ ra việc học của học viên có được ghi nhận dựa trên kết quả tương tác với SCO
     
     - **credit**: **Default**, "credit" affects the determination of the **cmi.success_status**
     
     - **no-credit**: no affects the determination of the **cmi.success_status**
   
   - **Mode and Credit Relationship**
     
     | cmi.mode value | cmi.credit value        |
     | -------------- | ----------------------- |
     | “browse”       | “no-credit”             |
     | “review”       | “no-credit”             |
     | “normal”       | “credit” or “no-credit” |
   
   - **GetValue("cmi.credit")**: default return "credit", EC = 0
   
   - **SetValue()**: return false, EC = 404

4. **Completion Status** - **cmi.completion_status**
   
   - Read / Write
   
   - Mandatory
   
   - Data Type: state (completed, incomplete, not_attempted, unknown)
   
   - LMS must override "cmi.completion_status" if "**cmi.completion_threshold**" and "**cmi.progress_measure**" is reported. Chi tiết xem ở "**4.2.4.1 Completion Status Determination**" trong file **scormruntime.pdf**
   
   - Indicates whether the learner has completed the SCO
     
     - **completed**
     
     - **incomplete**
     
     - **not attempted**: not to have used the SCO
     
     - **unknown**: **Default**
   
   - **GetValue(“cmi.completion_status”)**: default return "unknown" until it has been determined by the SCO or LMS, EC = 0
   
   - **SetValue(“cmi.completion_status”,”incomplete”)**: nếu giá trị khác 4 giá trị có sẵn => return false, EC = 406

5. **Entry** - **cmi.entry**
   
   - Read / Write
   
   - Mandatory
   
   - Data Type: state (ab_initio, resume, "")
   
   - Assert whether the learner has previously accessed the SCO
     
     - **ab-initio**: chỉ định đây là phiên học đầu tiên, **Default**
     
     - **resume**: người học đã học trước đây, nếu **cmi.exit** = **suspend** or **logout**
     
     - **""**: các trường hợp khác, vd: SCO đã được hoàn thành
   
   - **GetValue(“cmi.entry”)**: EC = 0
   
   - **SetValue()**: return false, EC = 404

6. **Exit** - **cmi.exit**
   
   - Write-only
   
   - Mandatory
   
   - Data Type: state (timeout, suspend, logout, normal, "") 
   
   - Indicates how or why the learner left the SCO
     
     - **time-out**: SCO terminated because time limit exceed **cmi.max_time_allowed**
     
     - **suspend**: học viên có thể quay lại điểm thoát để học tiếp
     
     - **logout**: terminate the entire learning activity include SCO
     
     - **normal**
     
     - **""**: The exit conditions are undetermined, **Default**
   
   - **GetValue()**: return "", EC = 405
   
   - **SetValue(“cmi.exit”,”suspend”)**: return true, EC = 0. Nếu giá trị khác giá trị có sẵn => return false, EC = 406

7. **ScoreScaled** - **cmi.score.scaled**
   
   - Read/ Write
   
   - Mandatory
   
   - Data Type: real (10,7) range (-1..1)
   
   - Number that reflects the performance of the learner
   
   - Impacts to **cmi.objectives.n.success_status** and **cmi.objectives.n.score.scaled**
   
   - SCO must ensure score information is accurately sent to the LMS ( SetValue() ) prior to the SCO’s learner session ending
   
   - **GetValue("cmi.score.scaled")**: EC = 0. If **cmi.score.scaled** is request before it is set => EC = 403
   
   - **SetValue(“cmi.score.scaled”,”0.750033”)**: return true, EC = 0
     
     - Value is not a real number, return false, EC = 406
     
     - Value is a real number but not in range [-1; 1], return false, EC = 407

8. **ScoreRaw** - **cmi.score.raw**
   
   - Read/ Write
   
   - Mandatory
   
   - Data Type: real (10,7)
   
   - Số phản ánh hiệu suất của người học so với phạm vi từ min -> max
   
   - Có thể được tính theo bất kỳ cách nào, miễn phù hợp với SCO. VD: tỷ lệ phần trăm các mục đã hoàn thành, điểm của bài kiểm tra trắc nghiệm, số câu trả lời đúng đầu tiên cho các câu hỏi trong SCO
   
   - **GetValue(“cmi.score.raw”)**: EC = 0
     
     - Nếu **cmi.score.raw** được request trước khi giá trị được đặt bởi SCO, return empty "", EC = 403
   
   - **SetValue(“cmi.score.raw”,”75.0033”)**: return true. EC = 0
     
     - Nếu giá trị được đặt không phải real number, return false, EC = 406

9. **ScoreMin** - **cmi.score.min**
   
   - Read/ Write
   
   - Mandatory
   
   - Data Type: real (10,7)
   
   - Giá trị nhỏ nhất trong phạm vi score raw
   
   - **GetValue(“cmi.score.min”)**: EC = 0
     
     - Nếu cmi được request trước khi giá trị được đặt bởi SCO, return empty "", EC = 403
   
   - **SetValue(“cmi.score.min”,”500”)**: return true. EC = 0
     
     - Nếu giá trị được đặt không phải real number, return false, EC = 406

10. **ScoreMax** - **cmi.score.max**
    
    - Read/ Write
    
    - Mandatory
    
    - Data Type: real (10,7)
    
    - Giá trị lớn nhất trong phạm vi score raw
    
    - **GetValue(“cmi.score.max”)**: EC = 0
      
      - Nếu cmi được request trước khi giá trị được đặt bởi SCO, return empty "", EC = 403
    
    - **SetValue(“cmi.score.max”,”500”)**: return true. EC = 0
      
      - Nếu giá trị được đặt không phải real number, return false, EC = 406

11. **Progress Measure** - **cmi.progress_measure**
    
    - Read/ Write
    
    - Mandatory
    
    - Data Type: real (10,7) range (0..1)
    
    - Thước đo tiến độ mà học viên đạt được
    
    - Relationship between **cmi.progress_measure** and **cmi.completion_status**
      
      | cmi.progress_measure | cmi.completion_status                                                                                                   |
      | -------------------- | ----------------------------------------------------------------------------------------------------------------------- |
      | 1                    | not attempted                                                                                                           |
      | 1                    | completed                                                                                                               |
      | 0 < value < 1        | imcomplete. unless **cmi.completion_threshold** is defined and **cmi.progress_measure** >= **cmi.completion_threshold** |
    
    - SCO define requirement for determine progress measure. Base on completion of a number of objectives
    
    - **GetValue(“cmi.progress_measure”)**: EC = 0
      
      - Nếu cmi được request trước khi giá trị được đặt bởi SCO, return empty "", EC = 403
    
    - **SetValue(“cmi.progress_measure”,”0.75”)**: return true. EC = 0
      
      - Nếu giá trị được đặt không phải real number, return false, EC = 406
      - Nếu giá trị là số thực nhưng nằm ngoài phạm vi [0; 1], return false, EC = 407

12. **Scaled Passing Score** - **cmi.scaled_passing_score**
    
    - Read - only
    
    - Mandatory
    
    - Data Type: real (10,7) range (0..1)
    
    - Điểm vượt qua được chia tỉ lệ bắt buộc để thành thạo SCO
    
    - Giá trị được chia tỉ lệ sao cho nằm trong khoảng [-1; 1]
    
    - LMS is responsible for initializing this element. Sử dụng dựa trên IMS Simple Sequencing namespace <<imsss:minNormalizedMeasure>> associated with        <<imsss:primaryObjective>> for <<imscp:item>> . Giá trị được defined trong manifest
    
    - **GetValue(“cmi.scaled_passing_score”)**: EC = 0
      
      - Nếu không có giá trị nào trong manifest nhưng SCO yêu cầu, return empty "", EC = 403
    
    - **SetValue()**: return false, EC = 404

13. **Total Time** - **cmi.total_time**
    
    - Read - only
    
    - Mandatory
    
    - Data Type: timeinterval (second,10,2) - tính đến  0,01s
    
    - sum of all of the learner’s session times (**cmi.session_time**)
    
    - Giá trị total time sẽ không được cập nhật nếu phiên học đã diễn ra
    
    - Khi SCO Terminate("") hoặc người dùng rời đi, LMS sẽ lấy giá trị **cmi.session_time** cuối cùng để cộng vào total time
    
    - Default value: PT0H0M0S
    
    - **GetValue(“cmi.total_time”)**: EC = 0
      
      - Nếu SCO không đặt bất kỳ sessions times, LMS không thể xác định total time -> return default value, EC = 0
    
    - **SetValue()**: return false, EC = 404

14. **Session Time** - **cmi.session_time**
    
    - Write - only
    
    - Mandatory
    
    - Data Type: timeinterval (second,10,2)
    
    - Tổng thời gian mà học viên đã học trong phiên hiện tại
    
    - Nếu không có phiên học nào được diễn ra, session time sẽ tính theo phiên học cuối cùng của học viên đối với SCO
    
    - SCO quyết định giá trị và cách thức tính session
    
    - **GetValue()**: return empty "", EC = 405
    
    - **SetValue(“cmi.session_time”,”PT1H5M”)**:  return true, EC = 0
      
      - Giá trị truyền vào không hợp lệ, return false, EC = 406

15. **Maximum Time Allowed** - **cmi.max_time_allowed**
    
    - Read - only
    
    - Mandatory
    
    - Data Type: timeinterval (second,10,2)
    
    - Lượng thời gian mà học viên được phép học trong SCO
    
    - Tính từ phiên học đầu tiên cho đến khi kết thúc
    
    - LMS is responsible for initializing. Giá trị của cmi này có thể tìm trong Content Package Manifest kết hợp với content organization. 
    
    - LMS sẽ sử dụng phần tử <imsss: tryAbsoluteDurationLimit>, nếu được xác định cho <imscp: item> tham chiếu đến Tài nguyên SCO trong tệp kê khai, để khởi tạo giá trị này.
    
    - **GetValue(“cmi.max_time_allowed”)**: EC = 0
      
      - Nếu không thấy cmi này trong manifest, khi SCO yêu cầu LMS set EC = 403, return empty ""
    
    - **SetValue()**: return false, EC= 404

16. **Time Limit Action** - **cmi.time_limit_action**
    
    - Read - only
    
    - Mandatory
    
    - Data Type: state (exit_message, continue_message,
      exit_no_message, continue_no_message)
    
    - Chỉ định SCO nên làm gì khi **cmi.max_time_allowed** bị vượt quá
      
      - **exit,message**: học viên buộc phải thoát, SCO gửi message: đã vượt qua thời gian tối đa cho phép
      
      - **continue,message**: học viên được tiếp tục học, SCO gửi message đã vượt qua thời gian cho phép
      
      - **exit,no message**: học viên buộc phải thoát, SCO không gửi message
      
      - **continue,no message**: học viên vẫn tiếp tục học mà không có message nào được gửi. **Default**
    
    - LMS is responsible for initializing. Giá trị sử dụng namespace ADL Content Package <adlcp: timeLimitAction>
    
    - Chỉ đặt trong <imscp: item> reference SCO resource, trong Content Package Manifest. Nếu không có <adlcp: timeLimitAction> được chỉ định, LMS dùng default value **continue,no message**
    
    - **GetValue(“cmi.time_limit_action”)**: EC = 0
      
      - Nếu ko có time limit nào được xác định trong manifest, dùng default value, EC = 0
    
    - **SetValue()**: return false, EC = 404

17. **Success Status** - **cmi.success_status**
    
    - Read/ Write
    
    - Mandatory
    
    - Data Type: state (passed, failed, unknown)
    
    - Chỉ định liệu rằng học viên đã thành thạo SCO. Dựa trên tỷ lệ tương tác, tỷ lệ đáp ứng objectives, tổng điểm của bài kiểm tra, v.v
      
      - **passed**: khi đạt số lượng objectives cần thiết hay số điểm cần thiết
      
      - **failed**:  khi không đạt lượng objectives cần thiết hay số điểm cần thiết
      
      - **unknown**: không có đánh giá, Default
    
    - **GetValue(“cmi.success_status”)**: EC = 0
      
      - Cho đến khi có yếu tố xác định thì giá trị mặc định là "unknown"
    
    - **SetValue(“cmi.success_status”,”passed”)**: nếu giá trị không thuộc 3 trường hợp trên => EC = 406

18. **Completion Threshold** - **cmi.completion_threshold**
    
    - Read/ Only
    
    - Mandatory
    
    - Data Type: real(10,7) range (0..1) , 0.0 <= cmi.completion_threshold <= 1.0
    
    - Xác định xem SCO có được coi là hoàn thành hay không. Bằng cách so sánh 
      
      **cmi.completion_threshold** với **cmi.progress_measure**
    
    - LMS is responsible for initializing. Giá trị sử dụng namespace ADL Content Package <adlcp: completionThreshold>
    
    - Có thể tìm trong <imscp: item> trong Content Package Manifest
    
    - **GetValue(“cmi.completion_threshold”)**: EC = 0
      
      - Khi SCO truy xuất, nếu không tìm thấy <<adlcp:completionThreshold>>, LMS set EC = 403, return empty ""
    
    - **SetValue()**: return false, EC = 404

19. **Suspend Data** - **cmi.suspend_data**
    
    - Read/ Write
    
    - Mandatory
    
    - Data Type: characterstring (SPM: 4000), ISO-10646-1
    
    - Trong quá trình học, học viên có thể tạm dừng (suspend) attempt trên SCO và tiếp tục sau đó. SCO sẽ cung cấp cơ chế tạm dừng, lúc này trạng thái của SCO's run-time data sẽ tồn tại cho đến phiên học tiếp theo (**cmi.exit** set to **suspend**) 
    
    - **cmi.suspend_data** dùng để lưu trữ data này, suspend data có thể được dùng bởi SCO ở lần resume tiếp theo
    
    - Nếu **cmi.exit** không được set **suspend** trước phiên học của học viên kết thúc, lần attempt của học viên sẽ kết thúc => tạo ra exception SCO và Persist State
      
      => state của **cmi.suspend_data** sẽ không có sẵn vào lần khởi chạy tiếp theo của SCO trong tương lai
    
    - Cmi này được tạo bởi SCO khi học viên truy cập hoặc tương tác. Format content không được xác định, LMS chỉ cần lưu dữ liệu
    
    - **GetValue()**: EC = 0
      
      - Nếu SCO không set value trước khi request => return empty "", EC = 403
    
    - **SetValue(“cmi.suspend_data”,”1001A”)**
      
      **SetValue(“cmi.suspend_data”,”A1;B2;C11-3”)**
      
      - Return true, EC = 0

20. **Launch Data** - **cmi.launch_data**
    
    - Read/ Only
    
    - Mandatory
    
    - Data Type: characterstring (SPM: 4000)
    
    - Cung cấp cho SCO thông tin chỉ định, để SCO sử dụng cho việc initialization
    
    - Giá trị của cmi này không được chỉ định
    
    - Nếu launch data được yêu cầu để khởi chạy SCO, dữ liệu được để trong ADL Content Package <adlcp: dataFromLMS>. Element có thể kết hợp với SCO resource  <imscp: item>  để xác định SCO được reference
    
    - LMS sẽ khởi tạo giá trị cmi.launch_data bằng cách sử dụng giá trị được cung cấp trong <adlcp: dataFromLMS>
    
    - **GetValue(“cmi.launch_data”)**: EC = 0
      
      - nếu không thấy <adlcp: dataFromLMS> cho <imscp: item>, LMS không chịu trách nhiệm cho giá trị khởi tạo của **cmi.launch_data**. Nếu SCO yêu cầu, LMS set EC = 403, return empty ""
    
    - **SetValue()**: return false, EC = 404

21. **Learner Id** - **cmi.learner_id**
    
    - Read/ Only
    
    - Data Type: long_identifier_type
    
    - **GetValue(“cmi.learner_id”)**

22. **Learner Name** - **cmi.learner_name**
    
    - Read/ Only
    
    - Data Type: localized_string_type (SPM: 250)
    
    - **GetValue(“cmi.learner_name”)**
