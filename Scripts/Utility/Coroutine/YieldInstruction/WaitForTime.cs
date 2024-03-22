 using System;
 using UnityEngine;

 public class WaitForTime : CustomYieldInstruction
 {
     // 1초의 틱 (고정값)
     readonly float TicksPerSecond = TimeSpan.TicksPerSecond;

     private float seconds;
     public float Seconds => seconds;

     // x초 후의 틱
     private long _end;
     private bool _isChanged;
     
     public WaitForTime(float seconds)
     {
         // 생성 시점 틱(가변값) + x 초 후 틱 (고정값)
         this.seconds = seconds <= 0 ? 0.0001f : seconds;

         UpdateTimer();
     }

     public WaitForTime WaitForSeconds(float seconds)
     {
         this.seconds = seconds <= 0 ? 0.0001f : seconds;

         UpdateTimer();
         return this;
     }
     // 대기조건

     private bool KeepWaiting()
     {
         if (!_isChanged)
         {
             _isChanged = true;
             seconds = seconds <= 0 ? 0.0001f : seconds;

             UpdateTimer();
         }
         
         return DateTime.Now.Ticks < _end;
     }

     public override void Reset()
     {
         _isChanged = false;
         UpdateTimer();
     }

     public override bool keepWaiting => KeepWaiting();

     private void UpdateTimer()
     {
         _end = DateTime.Now.Ticks + (long)(TicksPerSecond * seconds);
     }
 }