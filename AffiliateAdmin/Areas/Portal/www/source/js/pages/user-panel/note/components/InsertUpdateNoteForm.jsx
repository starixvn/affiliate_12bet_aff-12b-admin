import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, FormFeedback, Modal, ModalHeader, ModalBody } from 'reactstrap';
import { setShowPopup, setNoteList } from '../actions';
import { toggleLoader } from '../../../../actions/loaderAction';
import { post } from '../../../../modules/ajaxRequest';

import { resource } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

export class InsertUpdateNoteForm extends Component {
    constructor(props) {
        super(props);

        this.state = {
            noteContent: ''
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if (param !== prevProps.param) {
            this.setState({ noteContent: param.NoteContent })
        }
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value })
    }

    _validateNoteContent = () => {
        const { noteContent } = this.state;

        const result = {
            isValid: !!noteContent && noteContent.length <= resource.MaxLength,
            message: ''
        }

        if (!result.isValid) {
            result.message = !noteContent ? resource.RequiredErrorMessage : resource.MaxLengthErrorMessage;
        }

        return result;
    }

    handleSave = () => {
        const param = Object.assign({}, this.props.param, { NoteContent: this.state.noteContent });
        this.Save(param);
    }

    async Save(param) {
        this.props.toggleLoader(true);

        var url = `Portal/UserPanel/Note/UpdateNote`;;
        var message = resource.UpdateNoteSuccess;

        if (param.NoteId == 0) {
            url = `Portal/UserPanel/Note/InsertNote`;
            message = resource.AddNoteSuccess;
        }

        const response = await post(url, param);

        if (response) {
            if (response.data.IsSuccessful) {
                alert(message);

                this.props.setNoteList([]);
                this.props.setShowPopup(false);
            }
            else {
                alert(response.data.Message);
            }
        }

        this.props.toggleLoader(false);
    }

    render() {
        const { isShowPopup, param } = this.props;
        const popupTitle = param.NoteId == 0 ? resource.AddNote : resource.UpdateNote;
        const buttonTitle = param.NoteId == 0 ? globalResource.Add : globalResource.Update;

        const noteContentValidation = this._validateNoteContent();

        return (
            <Modal isOpen={isShowPopup} toggle={() => this.props.setShowPopup(false)} backdrop='static'>
                <ModalHeader toggle={() => this.props.setShowPopup(false)}><span className="iconpopup"></span>{popupTitle}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="note-form-container">
                            <Form tag="form" className="mb-5" autocomplete="off">
                                <FormGroup row className="mb-2">
                                    <Label for="noteContent" sm={4} className="required">Content</Label>
                                    <Col sm={5}>
                                        <Input type="textarea" name="noteContent" id="noteContent"
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

function mapStateToProps({ isShowPopup, param }) {
    return Object.assign({}, { isShowPopup, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setShowPopup, toggleLoader, setNoteList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(InsertUpdateNoteForm);