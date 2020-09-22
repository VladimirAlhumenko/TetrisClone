using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController {
    private Shape _model;
    private ShapeView _view;

    public ShapeController(Shape model, ShapeView view) {
        _model = model;
        _view = view;
    }

    public void ReceiveInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            DoAction(shp => shp.Move(new Cell(0, -1)));
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            DoAction(shp => shp.Move(new Cell(0, 1)));
        }
       
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            DoAction(shp => shp.Drop());
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            DoAction(shp => shp.Rotate());
        }
    }

    public IEnumerator Fall(Action<bool> onFall) {
        if (!_model.Falling) {
            _model.Falling = true;
            yield return new WaitForSeconds(1 / _model.Speed);

            var moved = _model.Move(new Cell(1, 0));
            _view.PlaceBlocks(_model.GetCurrentBlocks());

            onFall(!moved);

            _model.Falling = false;
        }
    }

    private void DoAction(Func<Shape, bool> action) {
        var actionPerformed = action(_model);

        if (actionPerformed) {
            _view.PlaceBlocks(_model.GetCurrentBlocks());
        }
    }

}
