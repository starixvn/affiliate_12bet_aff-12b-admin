import { trimText } from "../../../../modules/htmlHelper";

export const validate = (selectedRequest) => {
    const valRequireFieldResult = validateRequireField(selectedRequest);
    const valMaxLengthResult = validateMaxLength(selectedRequest);
    const valDeadlineResult = validateDeadline(selectedRequest);
    let messageList = [...valRequireFieldResult.messageList, ...valMaxLengthResult.messageList,
        ...valDeadlineResult.messageList
        ];
    const isValid = messageList.length === 0;
    if (messageList.length > 0)
    {
        messageList = ['Please check the error on the following field(s)', ...messageList];
    }
    return {isValid, messageList};
}

const validateRequireField = (selectedRequest) => {
    const userName = trimText(selectedRequest && selectedRequest.UserName);
    const agentName = trimText(selectedRequest && selectedRequest.AgentName);

    let isValid = true;
    const messageList = [];
    if (userName.length === 0)
    {
        isValid = false;
        messageList.push("- Affiliate")
    }
    if (agentName.length === 0)
    {
        isValid = false;
        messageList.push("- Agent Name")
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
        messageList.push("- Affiliate");
    }

    textAreaNames.forEach(name => {
        let text = trimText(selectedRequest && selectedRequest[name]);
        if (checkExceedMaxLength(text, 1000))
            messageList.push(`- ${name}`);
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
        messageList.push('- Deadline');
    }
    else{
        let date = new Date(deadline);
        if (date < now)
        {
            messageList.push('- Deadline')
        }
    }

    if (messageList.length > 0)
    {
        isValid = false;
    }
    return {isValid, messageList};
}

const checkExceedMaxLength = (text, maxLength) => text.length > maxLength;
