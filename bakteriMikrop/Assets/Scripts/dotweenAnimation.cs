using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class dotweenAnimation : MonoBehaviour
{
   [Header("Rotate")]
   public bool doRotate = false;
   public float rotateDuration = 1f;
   public Vector3 rotateAmount;
   public bool doRotateLoop = false;
   public LoopType rotateLoopType = LoopType.Yoyo;
   public int rotateLoopCount = -1;
   public Ease rotateEase = Ease.Linear;
   public bool isRotateRelative = false;

   [Header("Move")]
   public bool doMove = false;
   public float moveDuration = 1f;
   public Vector3 moveAmount;
   public bool doMoveLoop = false;
   public LoopType moveLoopType = LoopType.Yoyo;
   public int moveLoopCount = -1;
   public Ease moveEase = Ease.Linear;
   public bool isMoveRelative = false;
   
   [Header("Scale")]
   public bool doScale = false;
   public float scaleDuration = 1f;
   public Vector3 scaleAmount;
   public bool doScaleLoop = false;
   public LoopType scaleLoopType = LoopType.Yoyo;
   public int scaleLoopCount = -1;
   public Ease scaleEase = Ease.Linear;
   public bool isScaleRelative = false;



   void Start()
   {
      //Rotate
      if(doRotate)
      {
         if(doRotateLoop)
         {
            if(isRotateRelative) transform.DORotate(transform.rotation.eulerAngles + rotateAmount, rotateDuration).SetLoops(rotateLoopCount, rotateLoopType).SetEase(rotateEase);
            else transform.DORotate(rotateAmount, rotateDuration).SetLoops(rotateLoopCount, rotateLoopType).SetEase(rotateEase);
         }
         else
         {
            if(isRotateRelative) transform.DORotate(transform.rotation.eulerAngles + rotateAmount, rotateDuration).SetEase(rotateEase);
            else transform.DORotate(rotateAmount, rotateDuration).SetEase(rotateEase);
         }
      }
      
      //Move
      if(doMove)
      {
         if(doMoveLoop)
         {
            if(isMoveRelative) transform.DOMove(transform.position + moveAmount, moveDuration).SetLoops(moveLoopCount, moveLoopType).SetEase(moveEase);
            else transform.DOMove(moveAmount, moveDuration).SetLoops(moveLoopCount, moveLoopType).SetEase(moveEase);
         }
         else
         {
            if(isMoveRelative) transform.DOMove(transform.position + moveAmount, moveDuration).SetEase(moveEase);
            else transform.DOMove(moveAmount, moveDuration).SetEase(moveEase);
         }
      }
      
      
      //Scale
      if(doScale)
      {
         if(doScaleLoop)
         {
            if(isScaleRelative) transform.DOScale(transform.localScale + scaleAmount, scaleDuration).SetLoops(scaleLoopCount, scaleLoopType).SetEase(scaleEase);
            else transform.DOScale(scaleAmount, scaleDuration).SetLoops(scaleLoopCount, scaleLoopType).SetEase(scaleEase);
         }
         else
         {
            if(isScaleRelative) transform.DOScale(transform.localScale + scaleAmount, scaleDuration).SetEase(scaleEase);
            else transform.DOScale(scaleAmount, scaleDuration).SetEase(scaleEase);
         }
      }
   }
}
