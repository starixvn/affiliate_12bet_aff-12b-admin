import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, FormFeedback, Modal, ModalHeader, ModalBody } from 'reactstrap';
import { setShowPopup } from '../actions';
import { toggleLoader } from '../../../../actions/loaderAction';

import { globalResource } from '../../../../modules/globalResource';

const requiredErrorMessage = 'This field is required';
const maxLength = 1000;
const maxLengthErrorMessage = 'This field must not exceed 1000 characters';
export class InsertUpdateNoteForm extends Component {
    constructor(props) {
        super(props);

        const {param} = this.props;
        this.state = {
            noteContent: param.NoteContent
        }
    }

    handleChange = (event) => {
        this.setState({ value: event.target.value });
    }

    async handleSave() {
        if (this._validatePageCode().isValid) {
            this.props.toggleLoader(true);

            const param = Object.assign({}, this.props.param, { NoteContent: this.state.noteContent });

            var url = `Portal/UserPanel/Note/UpdateNote`;;

            if( param.NoteId == 0 ) {
                url = `Portal/UserPanel/Note/InsertNote`;
            }

            const response = await post(url, param);

            if (response) {
                if (response.data.IsSuccessful) {
                    alert(resource.ChangePasswordSuccess);

                    this.props.setShowPopup(false);
                }
                else {
                    alert(response.data.Message);
                }
            }

            this.props.toggleLoader(false);
        }
    }

    _validateNoteContent() {
        const { noteContent } = this.state;
        console.log(noteContent);
        const result = {
            isValid: !!noteContent && noteContent.length <= maxLength,
            message: ''
        }

        if (!result.isValid) {
            result.message = !noteContent ? requiredErrorMessage : maxLengthErrorMessage;
        }

        return result;
    }

    render() {
        const { isShowPopup, param } = this.props;

        const noteContentValidation = this._validateNoteContent();

        return (
            <Modal isOpen={isShowPopup} toggle={() => this.props.setShowPopup(false)} backdrop='static'>
                <ModalHeader toggle={() => this.props.setShowPopup(false)}><span className="iconpopup"></span>Edit</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="change-password-form-container">
                            <Form tag="form" className="mb-5" autocomplete="off">
                                <FormGroup row className="mb-2">
                                    <Label for="noteContent" sm={4} className="required">Content</Label>
                                    <Col sm={5}>
                                        <Input type="textarea" name="noteContent" id="noteContent"
                                            value={param.NoteContent} onChange={this.handleChange} invalid={!noteContentValidation.isValid}/>
                                        {noteContentValidation.message &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: noteContentValidation.message }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>
                            </Form>

                            <Button type="button" color="success" className="mr-3" disabled={!noteContentValidation.isValid} onClick={this.handleSave}>{globalResource.Submit}</Button>
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
    return bindActionCreators({ setShowPopup }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(InsertUpdateNoteForm);