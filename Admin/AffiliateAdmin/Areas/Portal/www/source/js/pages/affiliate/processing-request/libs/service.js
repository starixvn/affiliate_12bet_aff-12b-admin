import { trimText } from "../../../../modules/htmlHelper";
import{field} from './constants';

export const validate = (selectedRequest, originalData) => {
    const valRequireFieldResult = validateRequireField(selectedRequest);
    const valMaxLengthResult = validateMaxLength(selectedRequest);
    const valDeadlineResult = validateDeadline(selectedRequest, originalData && originalData.Deadline);
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
    let isValid = true;
    const textAreaWithLimit = {
        ChangeFirstName: 100,
        ChangeLastName: 100,
        AgentName: 1000,
        PromotionMethod: 1000,
        Remark: 1000,
        ServerCountry: 1000,
        TargetMarket: 1000,
        WebAdvAff: 1000,
        WebRank: 1000,
        WebRegDate: 1000,
        WebSMLanguage: 1000,
        WebSMQuality: 1000,
        WebSMType: 1000
    }
    const userName = trimText(selectedRequest && selectedRequest.UserName);

    let isExceed = checkExceedMaxLength(userName, 30);
    if (isExceed)
    {
        messageList.push("- Affiliate");
    }

    Object.keys(textAreaWithLimit).map(name => {
        let text = trimText(selectedRequest && selectedRequest[name]);
        if (checkExceedMaxLength(text, textAreaWithLimit[name]))
            messageList.push(`- ${field[name]}`);
    });

    if (messageList.length > 0)
    {
        isValid = false;
    }
    return {isValid, messageList};
}

export const validateDeadline = (selectedRequest, currentDate, today) => {
    let isHasDeadLine = !!currentDate;
    let isValid = true;
    const messageList = [];
    const now = new Date(today);
    now.setDate(now.getDate() - 1)
    const deadline = selectedRequest && selectedRequest.Deadline;
    if (!deadline)
    {
        messageList.push('- Deadline');
    }
    else{
        let date = new Date(deadline);
        let compareDate = new Date(currentDate);
        if (date < now && (!isHasDeadLine || !equalDate(date, compareDate)))
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

const equalDate = (firstDate, secondDate) => {
    return firstDate.getFullYear() === secondDate.getFullYear() && firstDate.getMonth() === secondDate.getMonth() && firstDate.getDate() === secondDate.getDate();
}

const checkExceedMaxLength = (text, maxLength) => text.length > maxLength;
