import React from 'react';
import { Button, Label, Input, Col} from 'reactstrap';
import { globalResource } from '../../../../modules/globalResource';
import {messages} from '../libs/constants';

export const InputFile = (props) => {
    const { handleOnChangeFiles, files, isMultiple, className, id} = props;
    const handleOnClick = () => {
        $(`#${id}`).click();
    }

    let text = messages.NoFileChoosen;
    if (files && files.length === 1)
    {
        text = files[0].name;
    }
    if (files && files.length > 1)
    {
        text = `${files.length} files`
    }

    return (
        <div className={className} onClick={handleOnClick}>
            <Button type="button">{globalResource.ChooseFile}</Button>
            <Label className="ml-1">{text}</Label>
            <Input type="file"
                name="file" 
                id={id}
                hidden
                files={files}
                multiple={isMultiple}
                onChange={handleOnChangeFiles} accept="image/jpg, image/gif, image/png, image/jpeg" />
        </div>
    )
}
export default (React.memo(InputFile));