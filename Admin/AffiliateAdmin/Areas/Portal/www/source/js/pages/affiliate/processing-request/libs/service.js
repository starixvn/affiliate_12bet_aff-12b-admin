import { trimText } from "../../../../modules/htmlHelper";

export const validate = (selectedRequest) => {
    const valRequireFieldResult = validateRequireField(selectedRequest);
    const valMaxLengthResult = validateMaxLength(selectedRequest);
    const valDeadlineResult = validateDeadline(selectedRequest);
    const messageList = [...valRequireFieldResult.messageList, ...valMaxLengthResult.messageList,
        ...valDeadlineResult.messageList
        ];
    const isValid = messageList.length === 0;
    return {isValid, messageList};
}

const validateRequireField = (selectedRequest) => {
    const userName = trimText(selectedRequest && selectedRequest.UserName);
    const agentName = trimText(selectedRequest && selectedRequest.AgentName);

    let isValid = true;
    const messageList = [];
    if (userName.length === 0 || agentName.length === 0)
    {
        isValid = false;
        messageList.push("Not fill all require field")
    }

    return {isValid, messageList}
}

const validateMaxLength = (selectedRequest) => {
    const messageList = [];
    const isValid = true;
    const textAreaNames =['AgentName', 'PromotionMethod', 'Remark', 'ServerCountry', 'TargetMarket', 'WebAdvAff', 'WebRank', 'WebRegDate', 'WebSMLanguage', 'WebSMQuality', 'WebSMType'];
    const userName = trimText(selectedRequest && selectedRequest.UserName);
    let isExceed = checkExceedMaxLength(userName, 30);
    if (isExceed)
    {
        messageList.push("Affiliate name exceed max charaters length (30)");
    }

    textAreaNames.forEach(name => {
        let text = trimText(selectedRequest && selectedRequest[name]);
        if (checkExceedMaxLength(text, 1000))
            messageList.push(`Field ${name} exceed max length (1000)`);
    })
    if (messageList.length > 0)
    {
        isValid = false;
    }
    return {isValid, messageList};
}

export const validateDeadline = (selectedRequest) => {
    let isValid = true;
    const messageList = [];
    const now = new Date();
    now.setDate(now.getDate() - 1)
    const deadline = selectedRequest && selectedRequest.Deadline;
    if (!deadline)
    {
        messageList.push('Require Deadline');
    }
    else{
        let date = new Date(deadline);
        if (date < now)
        {
            messageList.push('Invalid deadline')
        }
    }

    if (messageList.length > 0)
    {
        isValid = false;
    }
    return {isValid, messageList};
}

const checkExceedMaxLength = (text, maxLength) => text.length > maxLength;
