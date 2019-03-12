import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Alert, Col, Button, Form, FormGroup, Label, Input, FormFeedback, Modal, ModalHeader, ModalBody } from 'reactstrap';
import { setShowPopup, getNoteList, toggleLoader, post } from '../actions';

import { resource, maxLength } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

export class InsertUpdateNoteForm extends Component {
    constructor(props) {
        super(props);

        this.state = {
            noteContent: '',
            isShowMessage: false,
            isSuccess: true,
            message: ''
        }
    }

    componentDidUpdate(prevProps) {
        const { noteInfo } = this.props;

        if (noteInfo !== prevProps.noteInfo) {
            this.setState({ noteContent: noteInfo.NoteContent })
        }
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value })
    }

    _validateNoteContent = () => {
        const { noteContent } = this.state;

        const result = {
            isValid: !!noteContent && noteContent.length <= maxLength,
            message: ''
        }

        if (!result.isValid) {
            result.message = noteContent ? resource.MaxLengthErrorMessage : resource.RequiredErrorMessage;
        }

        return result;
    }

    handleSave = () => {
        const newNoteInfo = Object.assign({}, this.props.noteInfo, { NoteContent: this.state.noteContent });
        this.Save(newNoteInfo);
    }

    async Save(noteInfo) {
        this.props.toggleLoader(true);
        this.setState({ isShowMessage: true });

        var url = noteInfo.NoteId == 0 ? 'Portal/UserPanel/Note/InsertNote' : 'Portal/UserPanel/Note/UpdateNote';
        var message = noteInfo.NoteId == 0 ? resource.AddNoteSuccess : resource.UpdateNoteSuccess;

        const response = await post(url, noteInfo);

        if (response) {
            const result = response.data;

            if (result.IsSuccessful) {
                const { noteInfo, pagingOption } = this.props;
                const param = Object.assign({}, { AffId: noteInfo.AffId, PageIndex: 1, PageSize: pagingOption.PageSize });
                this.props.getNoteList(param);

                window.setTimeout(() => this.props.setShowPopup(false), 3000);
            }

            this.setState({
                isSuccess: result.IsSuccessful,
                message: result.IsSuccessful ? message : result.Message
            }, () => window.setTimeout(this.hideMessage, 3000));
        } else {
            this.setState({
                isSuccess: false,
                message: resource.Failed
            }, () => window.setTimeout(this.hideMessage, 3000));
        }

        this.props.toggleLoader(false);
    }

    hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    render() {
        const { isShowMessage, isSuccess, message } = this.state;
        const { isShowPopup, noteInfo } = this.props;
        const popupTitle = noteInfo.NoteId == 0 ? resource.AddNote : resource.UpdateNote;
        const buttonTitle = noteInfo.NoteId == 0 ? globalResource.Add : globalResource.Update;

        const noteContentValidation = this._validateNoteContent();

        return (
            <Modal isOpen={isShowPopup} toggle={() => this.props.setShowPopup(false)} backdrop='static'>
                <ModalHeader toggle={() => this.props.setShowPopup(false)}><span className="iconpopup"></span>{popupTitle}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="note-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-5" autocomplete="off">
                                <FormGroup row className="mb-2">
                                    <Label for="noteContent" sm={3} className="d-flex justify-content-center align-items-center required">Content</Label>
                                    <Col sm={9}>
                                        <Input type="textarea" name="noteContent" id="noteContent" rows="8"
                                            value={this.state.noteContent} onChange={this.handleChange} invalid={!noteContentValidation.isValid} />
                                        {noteContentValidation.message &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: noteContentValidation.message }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>
                            </Form>
                            <Button type="button" color="success" className="mr-3" disabled={!noteContentValidation.isValid} onClick={this.handleSave}>{buttonTitle}</Button>
                            <Button type="button" color="info" onClick={() => this.props.setShowPopup(false)}>{globalResource.Cancel}</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ isShowPopup, noteInfo, pagingOption }) {
    return Object.assign({}, { isShowPopup, noteInfo, pagingOption });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setShowPopup, toggleLoader, getNoteList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(InsertUpdateNoteForm);