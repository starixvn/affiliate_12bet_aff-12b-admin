import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Alert, Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody } from 'reactstrap';
import { setSelectedMessageId, getMessageDetail, fetchData, insertMessage, updateMessage } from '../actions';

import { htmlDecode } from '../../../modules/htmlHelper';
import { resource, languages, popupTitle } from '../libs/constants';

export class MessageDetailForm extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isClickedEdit: false,
            important: false,
            messageContentList: {
                en: '',
                tw: '',
                cn: '',
                es: '',
                th: '',
                vi: '',
                id: '',
                kr: '',
                jp: ''
            },
            isShowMessage: false,
            isSuccess: true,
            message: ''
        }
    }

    componentDidUpdate(prevProps) {
        const { selectedMessageId } = this.props;

        if (selectedMessageId && selectedMessageId != 0 && selectedMessageId !== prevProps.selectedMessageId) {
            this._handleGetMessageDetail(selectedMessageId);
        }
    }

    _handleGetMessageDetail = async (selectedMessageId) => {
        const item = await this.props.getMessageDetail(selectedMessageId);

        this.setState({
            important: item.Important,
            messageContentList: {
                en: item.MessageContentEn,
                tw: item.MessageContentTw,
                cn: item.MessageContentCn,
                es: item.MessageContentEs,
                th: item.MessageContentTh,
                vi: item.MessageContentVi,
                id: item.MessageContentId,
                kr: item.MessageContentKr,
                jp: item.MessageContentJp
            }
        });
    }

    _handleChangeMessageContent = (event) => {
        const messageContentList = Object.assign({}, this.state.messageContentList, { [event.target.name]: event.target.value })
        this.setState({ messageContentList })
    }

    _handleSave = () => {

    }

    async _Save(messageInfo) {
        const isInsert = messageInfo.MessageId === 0;

        const message = isInsert ? resource.AddMessageSuccess : resource.UpdateMessageSuccess;

        const result = isInsert ? await this.props.insertMessage(messageInfo) : await this.props.updateMessage(messageInfo);

        if (result) {
            this.setState({
                isSuccess: result.IsSuccessful,
                isShowMessage: true,
                message: result.IsSuccessful ? message : result.Message
            }, () => window.setTimeout(this.hideMessage, 3000));

            if (result.IsSuccessful) {
            }
        } else {
            this.setState({
                isSuccess: false,
                isShowMessage: true,
                message: resource.Failed
            }, () => window.setTimeout(this._hideMessage, 3000));
        }
    }

    _hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    _handleClosePopup = () => {
        this.props.setSelectedMessageId(null);
        this.setState({ isClickedEdit: false });
    }

    render() {
        const { important, messageContentList, isClickedEdit, isSuccess, isShowMessage, message } = this.state;
        const { selectedMessageId } = this.props;
        const isShowPopup = selectedMessageId !== null;
        const isInsert = selectedMessageId == 0;
        const isCurrentMessage = selectedMessageId == -1;
        const title = isInsert ? popupTitle.Add : (isCurrentMessage ? popupTitle.Current : `${popupTitle.Detail} (ID=${selectedMessageId})`);
        const buttonTitle = !isInsert && isClickedEdit ? resource.Update : resource.Edit;

        return (
            <Modal isOpen={isShowPopup} size="lg" toggle={this._handleClosePopup} backdrop='static'>
                <ModalHeader toggle={this._handleClosePopup}><span className="iconpopup"></span>{title}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="message-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-2 p-1 border" autocomplete="off">
                                <FormGroup row className="mb-2">
                                    <Label for="important" sm={2} className="text-right">{resource.Important}</Label>
                                    <Col sm={10}>
                                        <Input name="important" type="checkbox" className="important-checkbox" checked={important} onChange={() => this.setState({ important: !important })} />
                                    </Col>
                                </FormGroup>
                                {
                                    Object.keys(messageContentList).map(key => {
                                        return (
                                            <FormGroup row className="mb-2">
                                                <Label for={key} sm={2} className="d-flex justify-content-end align-items-center">{languages[key]}</Label>
                                                <Col sm={10}>
                                                    <Input type="textarea" name={key} id={key} rows="6"
                                                        value={htmlDecode(messageContentList[key])} onChange={this._handleChangeMessageContent} disabled={false} />
                                                </Col>
                                            </FormGroup>
                                        )
                                    })
                                }
                            </Form>
                            {
                                isInsert ? (<Button type="button" color="success" className="mr-2" onClick={this._handleSave}>{resource.Add}</Button>)
                                    : (
                                        <React.Fragment>
                                            <Button type="button" color="success" className="mr-2" onClick={this._handleSave}>{buttonTitle}</Button>
                                            <Button type="button" color="danger" className="mr-2" onClick={this._handleSave}>{resource.Delete}</Button>
                                        </React.Fragment>
                                    )
                            }
                            <Button type="button" color="info" onClick={this._handleClosePopup}>{resource.Cancel}</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ selectedMessageId }) {
    return Object.assign({}, { selectedMessageId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedMessageId, getMessageDetail, fetchData, insertMessage, updateMessage }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(MessageDetailForm));